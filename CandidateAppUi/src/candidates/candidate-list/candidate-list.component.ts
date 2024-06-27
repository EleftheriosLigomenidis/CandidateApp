import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Subject, Observable, takeUntil } from 'rxjs';
import { Candidate } from 'src/models/Candidate';
import { CandidateService } from 'src/services/candidate.service';

@Component({
  selector: 'app-candidate-list',
  templateUrl: './candidate-list.component.html',
  styleUrls: ['./candidate-list.component.css']
})
export class CandidateListComponent implements OnInit {

  private destroy$ = new Subject<void>();
  candidates$!:Observable<Candidate[]>
  constructor(private readonly candidateService:CandidateService,private cd:ChangeDetectorRef) { 
    this.loadCandidates();
  }


  loadCandidates(){
    this.candidates$ = this.candidateService.getCandidates().pipe(takeUntil(this.destroy$))
  }

  delete(id:number){
    this.candidateService.deleteCandidate(id).subscribe({
      next:() => {
        this.loadCandidates(); 
        this.cd.markForCheck();
      },
      error:() => {
        
      }
    })
  }

  ngOnInit() {

  }
}
