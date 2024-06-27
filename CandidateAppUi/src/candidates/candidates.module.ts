import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';

//lazy loading   instead of loading all modules in app module
const routes: Routes = [
  {
    path: 'list',
    loadChildren:() => import('./candidate-list/candidate-list.module').then((x) => x.CandidateListModule)
  },
  {
    path: 'candidate/:id',
    loadChildren:() => import('./candidate-form/candidate-form.module' ).then((x) => x.CandidateFormModule)
  },

]


@NgModule({
  imports: [
    CommonModule,
  
    RouterModule.forChild(routes)
  ],
  declarations: []
})
export class CandidatesModule { }
