import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { Task } from '../task';

const baseUrl = "http://localhost:50107/api/";
//const baseUrl = "http://localhost/task-mgr/api/";
//const baseUrl = "http://localhost:50019/api/";
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':'application/json'
  })
};

@Injectable()
export class TaskService {

private messageSource = new BehaviorSubject( {
  TaskName: '',
  TaskId: 0,
  StartDate: '',
  EndDate: '',
  Priority: 1,
  Status: 'I',
  ParentId: 0,
  ParentTask: ''
});

currentTask = this.messageSource.asObservable();

constructor(private http: HttpClient) { }

getTaskList():Observable<object> {    
 return this.http.get<Task[]>(baseUrl + 'task/all');
}

getTask(taskId: number): Observable<Task> {
  return this.http.get<Task>(baseUrl + 'task/' + taskId);
}

saveTask(newTask: Task): Observable<any> {
  newTask.Status = 'I';
  let body = JSON.stringify(newTask);  
  let httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':'application/json'
    })
  };  
  return this.http.post(baseUrl + 'task/add',body, httpOptions);
}

endTask(taskId: number): Observable<any> {
  let params = new HttpParams()
  .set("taskId",'' + taskId);
  return this.http.get(baseUrl + 'task/end',{ params: params});
}

selectTask(task: Task) {
  this.messageSource.next(task);
}

getParentTasks(taskId: number): Observable<any[]> {
  let params = new HttpParams()
  .set("taskId",'' + taskId);

  return this.http.get<Task[]>(baseUrl + 'task/parentlist',{ params: params});
}

}

