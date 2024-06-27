import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CandidateFormComponent } from './candidate-form.component';
import { RouterModule, Routes } from '@angular/router';
import { CandidateFormResolver } from '../candidate-form.resolver';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxSelectModule, INgxSelectOptions } from 'ngx-select-ex';

const CustomSelectOptions: INgxSelectOptions = { // Check the interface for more options
  optionValueField: 'id',
  optionTextField: 'name'
};

const routes:Routes = [
  {
    path:'',
    component:CandidateFormComponent,
    resolve:{
      result:CandidateFormResolver
    }
  }
  ]
@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    NgxSelectModule.forRoot(CustomSelectOptions),
    RouterModule.forChild(routes)
    
  ],
  declarations: [CandidateFormComponent]
})
export class CandidateFormModule { }
