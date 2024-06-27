import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DegreeFormComponent } from './degree-form.component';
import { RouterModule, Routes } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { DegreeFormResolver } from '../degree-form.resolver';


const routes:Routes = [
  {
    path:'',
    component:DegreeFormComponent,
    resolve:{
      degreeResult:DegreeFormResolver
    }
  }
  ]

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule
  ],
  declarations: [DegreeFormComponent]
})
export class DegreeFormModule { }
