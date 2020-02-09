import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormsModule } from '@angular/forms';
import { TaskService } from 'src/app/services/task.service';
import { HttpClient } from '@angular/common/http';
import { HttpHandler } from '@angular/common/http/src/backend';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Task } from '../task';
import { switchMap } from 'rxjs/operators';
import { Observable } from 'rxjs/internal/Observable';

const serviceUrl = "http://localhost:50019/task/add";

@Component({
  selector: 'app-edittask',
  templateUrl: './edittask.component.html',
  styleUrls: ['./edittask.component.css']
})

export class EdittaskComponent implements OnInit {
  task$: Observable<Task>;
  parentList: any[];

  @Input() task: Task;
  taskForm = this.fb.group({
    taskName: [''],
    priority: ['1'],
    parentId: ['0'],
    startDate: [''],
    endDate: ['']
  });
  constructor(private router: Router,private route: ActivatedRoute,private taskService: TaskService, private fb: FormBuilder) {     
  }

  submitTask() {    
    this.taskService.saveTask(this.task).subscribe(data => this.router.navigate(['/taskview']));    
    //console.log(this.task);
  }

  reset() {
    this.taskForm.reset();
  }
  ngOnInit() {
    this.taskService.currentTask.subscribe((task) =>{
      this.task = task;
      this.taskService.getParentTasks(this.task.TaskId).subscribe(parentList => this.parentList = parentList);
      console.log('task received');
      console.log(this.task);
    } );        
  }

}
