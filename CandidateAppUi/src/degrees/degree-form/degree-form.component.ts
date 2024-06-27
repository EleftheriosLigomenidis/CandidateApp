import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';
import { Degree } from 'src/models/Degree';
import { DegreeService } from 'src/services/degree.service';

@Component({
  selector: 'app-degree-form',
  templateUrl: './degree-form.component.html',
  styleUrls: ['./degree-form.component.css']
})
export class DegreeFormComponent implements OnInit {

  degreeForm!:FormGroup;
  degreeId:number = 0
  private destroy$ = new Subject<void>(); 
    constructor(private formBuilder:FormBuilder, 
                private route:ActivatedRoute,
                private router:Router,
                
              private  degreeService:DegreeService) { 
    }
  
    ngOnInit() {   
  
      this.route.params.subscribe(params => {
        this.degreeId = params['id'];
      const result = this.route.snapshot.data['degreeResult'];
      const degree: Degree = Object.assign(new Degree(), result)
      this.degreeForm =  degree.toFormGroup(this.formBuilder);
    })
    }
  
    onSubmit() {
      const formValue = this.degreeForm.getRawValue();
      const model = new Degree().toModel(formValue);
  
       if(this.degreeId == 0){
          this.degreeService.createDegree(model).subscribe({
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
        this.degreeService.updateDegree(model).subscribe({
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
  
    ngOnDestroy() {
      this.destroy$.next();
      this.destroy$.complete();
    }

}
