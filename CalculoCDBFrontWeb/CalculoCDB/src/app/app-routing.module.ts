import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CalculoCDBComponent } from './calculo-cdb/calculo-cdb.component';

const routes: Routes = [
  {path:'home', component:CalculoCDBComponent},
  {path:'', redirectTo:'home',pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes,{useHash:true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
