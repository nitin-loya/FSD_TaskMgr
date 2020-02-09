export interface Task {
    TaskId: number;
    TaskName: string;
    StartDate: string;
    EndDate: string;
    Priority: number;
    Status: string;
    ParentId: number;
    ParentTask: string;
}
