import {Component} from '@angular/core';
import {NgControl} from '@angular/common';
import {AppServiceTodoList} from '../../services/app.service.todolist';

@Component({
    selector: 'todolist',
    templateUrl: './app/components/todolist/todolist.component.html',
    styleUrls: ['./app/components/todolist/todolist.component.css']
})


export class TodoListComponent {
    constructor(private _appService: AppServiceTodoList) {
    }
     
    get hasSelectedList() {
        return this._appService.hasSelectedList;
    }

    get selectedList(): Array<Object> {
        return this._appService.selectedList;
    }

    get haserror() {
        return this._appService.haserror;
    }

    get errormsg() {
        return this._appService.errormsg;
    }
}