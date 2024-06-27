import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Observable, Subject, takeUntil } from 'rxjs';
import { Degree } from 'src/models/Degree';
import { DegreeService } from 'src/services/degree.service';

@Component({
  selector: 'app-degree-list',
  templateUrl: './degree-list.component.html',
  styleUrls: ['./degree-list.component.css']
})
export class DegreeListComponent implements OnInit {

  degrees$: Observable<Degree[]> | undefined;
  private destroy$ = new Subject<void>();

  
  constructor(private degreeService:DegreeService,private cd: ChangeDetectorRef) {
  
  }

  ngOnInit() {
      this.loadDegrees();
  }

  loadDegrees() {
    this.degrees$ = this.degreeService.getDegrees().pipe(
      takeUntil(this.destroy$)
    );
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }


  delete(id:number ){
    this.degreeService.deleteDegree(id).subscribe({
      next:() => {
        this.loadDegrees(); 
        this.cd.markForCheck();
      },
      error:() => {
        
      }
    })
  }
}
