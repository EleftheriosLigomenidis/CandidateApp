import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';



const routes: Routes = [
  {
    path: 'list',
    loadChildren:() => import('./degree-list/degree-list.module').then((x) => x.DegreeListModule)
  },
  {
    path: 'degree/:id',
    loadChildren:() => import('./degree-form/degree-form.module' ).then((x) => x.DegreeFormModule)
  },

]

@NgModule({
  imports: [
    CommonModule,   
     RouterModule.forChild(routes),
  ],
  declarations: []
})
export class DegreesModule { }
