import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: 'degrees',
    loadChildren: () =>
      import('../degrees/degrees.module').then((x => x.DegreesModule))
   },
   {
    path:'candidates',
    loadChildren: () =>
      import('../candidates/candidates.module').then((x => x.CandidatesModule))
   },
   {
    path:'home',
    loadChildren:() =>   import('../home/home.module').then((x => x.HomeModule))
   },
   { path: '',
     redirectTo: '/home',
    pathMatch: 'full' 
    },
    { path: '**',
      redirectTo: '/home',
     pathMatch: 'full' 
     }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
