import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TasklistComponent } from './tasklist/tasklist.component';
import { EdittaskComponent } from './edittask/edittask.component';

const routes: Routes = [
  { path: 'taskview', component: TasklistComponent},
  { path: 'edittask', component: EdittaskComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
