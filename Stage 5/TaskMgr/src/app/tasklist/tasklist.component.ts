import { Component, OnInit, EventEmitter,Output } from '@angular/core';
import { Task } from '../task';
import { TaskService } from 'src/app/services/task.service';
import {Observable } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-tasklist',
  templateUrl: './tasklist.component.html',
  styleUrls: ['./tasklist.component.css']
})
export class TasklistComponent implements OnInit {

  taskSource: any;
  taskList: any;
  filter: any;
  criteria: any;
  message: string;
  @Output() taskSelected = new EventEmitter<Task>();

  constructor(private router:Router, private service: TaskService) {     
  //  this.taskList = [
  //     {
  //       taskId: 1,
  //       taskName: 'Task1',
  //       startDate: '2019-03-12',
  //       endDate: '2019-03-17',
  //       priority: 1,
  //       status: 'I',
  //       parentTask: null
  //     },
  //     {
  //       taskId: 2,
  //       taskName: 'Task2',
  //       startDate: '2019-01-10',
  //       endDate: '2019-02-10',
  //       priority: 2,
  //       status: 'I',
  //       parentTask: null
  //     },
  //     {
  //       taskId: 3,
  //       taskName: 'Task3',
  //       startDate: '2019-04-12',
  //       endDate: '2019-04-20',
  //       priority: 2,
  //       status: 'I',
  //       parentTask: "Task1"
  //     }      
  //   ];    

    this.service.getTaskList()    
    .subscribe(response => {
      console.log(response); 
      this.taskList = response;
      this.taskSource = response;
    });    
  }

  filterGrid(filter) {
    this.taskList = this.taskSource;
    console.log(filter);    
    if(filter.TaskName != null) {
      this.taskList = this.taskSource.filter((task) => task.TaskName.indexOf(filter.TaskName) > -1);
      //console.log('Searching on name');
    }
   
    if(filter.ParentTask != null) {
      this.taskList = this.taskList.filter((task) => task.ParentTask && task.ParentTask.indexOf(filter.parentTask) > -1);
      //console.log('Searching on parent');
    }
      
   /* if(filter.priorityFrom != null) {
      this.taskList = this.taskList.filter(task => task.priority >= filter.priorityFrom);
      console.log('From' + filter.priorityFrom);
    }
    console.log(this.taskList);

    if(filter.priorityTo != null) {
      this.taskList = this.taskList.filter(task => task.priority <= filter.priorityTo);
      console.log('To' + filter.priorityTo);
    }

    if(filter.startDate != null) {
      this.taskList = this.taskList.filter(task => new Date(task.startDate) >= new Date(filter.startDate));
      console.log(filter.startDate);
    }

    if(filter.endDate != null) {
      this.taskList = this.taskList.filter(task => new Date(task.endDate) >= new Date(filter.endDate));
      console.log(filter.endDate);
    }*/
    console.log(this.taskList);
  }

  ngOnInit() {
  }

  onEdit (task: Task): void {
    this.service.selectTask(task);
    this.router.navigate(['/edittask']);
    console.log(task);
  }

  onEnd (task: Task): void {
    console.log('end');
    this.service.endTask(task.TaskId).subscribe((data) => {
      console.log('end completed');
      this.router.navigate(['/taskview']);
    })
  }
}
