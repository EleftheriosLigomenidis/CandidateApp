import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, forkJoin, of } from 'rxjs';
import { Candidate } from 'src/models/Candidate';
import { Degree } from 'src/models/Degree';
import { CandidateService } from 'src/services/candidate.service';
import { DegreeService } from 'src/services/degree.service';

@Injectable({
  providedIn: 'root'
})
export class CandidateFormResolver implements Resolve<Observable<{candidate:Candidate,degrees:Degree[]}>> {


  constructor(private candidateService:CandidateService,private degreeService:DegreeService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<{candidate:Candidate,degrees:Degree[]}>  {
    
    const id = +route.params['id'];
   const candidate$ = id != 0 ? this.candidateService.getCandidate(id) : of({ id: 0 } as Candidate)
   const degrees$ = this.degreeService.getDegrees();
    return forkJoin({
      degrees:degrees$,
      candidate:candidate$
    })
  }
}
