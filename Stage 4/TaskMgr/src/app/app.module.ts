import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule,FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { EdittaskComponent } from './edittask/edittask.component';
import { TasklistComponent } from './tasklist/tasklist.component';
import { TaskFilterComponent } from './task-filter/task-filter.component';
import { SearchPipe } from './search.pipe';
import { TaskService } from 'src/app/services/task.service';

@NgModule({
  declarations: [
    AppComponent,
    EdittaskComponent,
    TasklistComponent,
    TaskFilterComponent,
    SearchPipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
   // HttpClientTestingModule,
    ReactiveFormsModule,
    FormsModule
  ],
  providers: [TaskService],
  bootstrap: [AppComponent]
})
export class AppModule { }
