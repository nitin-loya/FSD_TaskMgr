import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'search'
})
export class SearchPipe implements PipeTransform {

  transform(tasks: any, criteria?: any): any {
    let filteredTasks = tasks;
    
    if(criteria == null) return filteredTasks;

    if(criteria.TaskName != null){
      filteredTasks = filteredTasks.filter(task => task.TaskName.indexOf(criteria.TaskName) !== -1);
    }
      
    if(criteria.ParentTask != null && criteria.ParentTask !== ''){
      filteredTasks = filteredTasks.filter(task => (task.ParentTask != null && task.ParentTask.indexOf(criteria.ParentTask) !== -1));
    }

    if(criteria.PriorityFrom != null){
      filteredTasks = filteredTasks.filter(task => (task.Priority >= criteria.PriorityFrom));
    }

    if(criteria.priorityTo != null){
      filteredTasks = filteredTasks.filter(task => (task.Priority <= criteria.PriorityTo));
    }

    //console.log(criteria.StartDate);
    if(criteria.StartDate != null) {
      filteredTasks = filteredTasks.filter(task => new Date(task.StartDate) >= new Date(criteria.StartDate));      
    }

    //console.log(criteria.endDate);
    if(criteria.EndDate != null) {
      filteredTasks = filteredTasks.filter(task => new Date(task.EndDate) <= new Date(criteria.EndDate));      
    }
    return filteredTasks;
  }

}
