import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DegreeListComponent } from './degree-list.component';
import { RouterModule, Routes } from '@angular/router';



const routes:Routes = [
  {
    path:'',
    component:DegreeListComponent
  }
  ]

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
  ],
  declarations: [DegreeListComponent]
})
export class DegreeListModule { }
