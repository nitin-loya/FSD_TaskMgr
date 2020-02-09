import { TestBed, getTestBed, inject, fakeAsync } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TaskService } from './task.service';
import { Task } from '../task';
import { ExpectedConditions } from 'protractor';

describe('TaskService', () => {  

  beforeEach(() => {    
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [TaskService]
    });
  });  
  
  const taskList: Task[] = [
    {
      taskId: 1,
      taskName: 'Task1',
      startDate: '12-Mar-2019',
      endDate: '17-Mar-2019',
      priority: 1,
      status: 'I',
      parentId: null
    },
    {
      taskId: 2,
      taskName: 'Task2',
      startDate: '10-Jan-2019',
      endDate: '10-Feb-2019',
      priority: 2,
      status: 'I',
      parentId: null
    },
    {
      taskId: 3,
      taskName: 'Task3',
      startDate: '12-Apr-2019',
      endDate: '20-Apr-2019',
      priority: 2,
      status: 'I',
      parentId: 1
    }      
  ];
   it('TaskService is created',
   inject([HttpTestingController, TaskService],(httpMock: HttpTestingController, service: TaskService) => {
      expect(service).toBeTruthy();
   })

   );

   it('getTaskList() should return Observable<Task[]>',   
    inject([HttpTestingController, TaskService], (httpMock: HttpTestingController, service: TaskService) => {
      const url = 'http://localhost/task/all';
	  //const url = 'http://localhost:50107/api/Task/All';
      const expectedResponse = taskList;
      let actualResponse = null;

      service.getTaskList().subscribe(receivedResponse => {
          actualResponse = receivedResponse;
          expect(actualResponse).toEqual(expectedResponse);
          //expect(actualResponse.status).toBe(200);
        },
        (error: any) => {}
      );

      
      const requestWrapper = httpMock.expectOne({url: url});
      requestWrapper.flush(expectedResponse);
      expect(requestWrapper.request.method).toEqual('GET');      
    })  
  );

    
  it('getTask() should return Observable<Task>',   
    inject([HttpTestingController, TaskService], (httpMock: HttpTestingController, service: TaskService) => {
      const url = 'http://localhost/task/2';
	  //const url = 'http://localhost:50107/api/task/2';
      const expectedResponse = {
        taskId: 2,
        taskName: 'Task2',
        startDate: '10-Jan-2019',
        endDate: '10-Feb-2019',
        priority: 2,
        status: 'I',
        parentId: null
      };
      let actualResponse = null;

      service.getTask(2).subscribe(receivedResponse => {
          actualResponse = receivedResponse;
          expect(actualResponse).toEqual(expectedResponse);
          //expect(actualResponse.status).toBe(200);
        },
        (error: any) => {}
      );

      
      const requestWrapper = httpMock.expectOne({url: url});
      requestWrapper.flush(expectedResponse);
      expect(requestWrapper.request.method).toEqual('GET');      
    })  
  );

  it('addTask() should add new task',   
    inject([HttpTestingController, TaskService], (httpMock: HttpTestingController, service: TaskService) => {
      const url = 'http://localhost/task/add';
	  //const url = 'http://localhost:50107/api/Task/add';
      const newTask = {
        taskId: 2,
        taskName: 'Task2',
        startDate: '10-Jan-2019',
        endDate: '10-Feb-2019',
        priority: 2,
        status: 'I',
        parentId: null
      };
      const expectedResponse = '<form />';
      let actualResponse = null;

      service.addTask(newTask).subscribe(receivedResponse => {
          actualResponse = receivedResponse;
          expect(actualResponse).toEqual(expectedResponse);
          //expect(actualResponse.status).toBe(200);
        },
        (error: any) => {}
      );

      
      const requestWrapper = httpMock.expectOne({url: url, method: 'POST'});
      requestWrapper.flush(expectedResponse);
      expect(requestWrapper.request.method).toEqual('POST');      
    })  
  );
}
);
