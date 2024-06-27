import { Attribute, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Candidate } from 'src/models/Candidate';
import { Degree } from 'src/models/Degree';
import { CandidateService } from 'src/services/candidate.service';

@Component({
  selector: 'app-candidate-form',
  templateUrl: './candidate-form.component.html',
  styleUrls: ['./candidate-form.component.css']
})
export class CandidateFormComponent implements OnInit {
  degrees$!:Observable<Candidate[]>
  candidateId!:number;
  candidateForm!:FormGroup
  degreeList:Degree[] = []
  selectedList:number[] = []
  file!:any;
  @ViewChild('uploadInput') uploadInput!: ElementRef;
    constructor(private candidateService:CandidateService,
      private formBuilder:FormBuilder, 
      private route:ActivatedRoute,
      private router:Router,
      ) { }
  
    ngOnInit() {
  
      this.route.params.subscribe(params => {
        this.candidateId = params['id']
        const result = this.route.snapshot.data['result'];
        const candidate: Candidate = Object.assign(new Candidate(), result.candidate)
        const degrees:Degree[] = result.degrees.map((x:any) => new Degree().toModel(x));
        this.degreeList = degrees;
        candidate.degrees = [...candidate.degrees.map(x => new Degree().toModel(x))];
        this.initialiseSelectedItems(candidate.degrees);

        this.candidateForm =  candidate.toFormGroup(this.formBuilder);
        
  
      })
  
  
    }
  initialiseSelectedItems(degrees:Degree[]):void{
    if(degrees.some(x  => x)){
      this.selectedList = degrees.map((x:Degree) => x.id);
    }
  }
    onFileChange(event:any): void {
      if (event.target.files.length > 0) {
        const file = event.target.files[0];
       this.file = file;
      }
    }
  
    closePopUp(){
      this.uploadInput.nativeElement.value = null;

    }

  mapDegrees(selectedItems:number[]):Degree[]{
    const selectedDegrees:Degree[] = this.degreeList.filter(x => selectedItems.includes(x.id));

    return selectedDegrees
  }

    onSubmit(){
      const formValue = this.candidateForm.getRawValue();
      debugger;
      this.selectedList;
      const model = new Candidate().toModel(formValue);
      
      model.degrees = this.mapDegrees(this.selectedList)
     
      if(this.candidateId == 0){
        this.candidateService.createCandidate(model).subscribe({
          next:() => {
            //success
          },
          error:() => {
            // error
          },complete:() => {
            this.router.navigate(['/degrees/list']);
          }
        })
     }else{
      this.candidateService.updateCandidate(model).subscribe({
        next:() => {
          //success
        },
        error:() => {
          // error
        },complete:() => {
          this.router.navigate(['/degrees/list']);
        }
      })
     }
    }
  
}


