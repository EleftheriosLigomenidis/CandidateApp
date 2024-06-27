import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CandidateListComponent } from './candidate-list.component';
import { RouterModule, Routes } from '@angular/router';
import { DegreeFormResolver } from 'src/degrees/degree-form.resolver';
import { DegreeFormComponent } from 'src/degrees/degree-form/degree-form.component';

const routes:Routes = [
  {
    path:'',
    component:CandidateListComponent
  }
  ]
@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  declarations: [CandidateListComponent]
})
export class CandidateListModule { }
