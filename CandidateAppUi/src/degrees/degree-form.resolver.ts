import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { Degree } from 'src/models/Degree';
import { DegreeService } from 'src/services/degree.service';

@Injectable({
  providedIn: 'root'
})
export class DegreeFormResolver implements Resolve<Observable<Degree>> {

  constructor(private degreeService: DegreeService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Degree> {

    const id = +route.params['id'];
   const degree$ = id != 0 ? this.degreeService.getDegree(id) : of({ id: 0 } as Degree)
    return degree$;
  }
}