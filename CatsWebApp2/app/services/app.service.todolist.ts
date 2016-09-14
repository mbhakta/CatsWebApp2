import {Injectable} from '@angular/core';
import {Http} from '@angular/http';
import {HttpHelpers} from '../utils/HttpHelpers';
import {Observable} from 'rxjs/Observable';
import 'rxjs/Rx';

@Injectable()
export class AppServiceTodoList extends HttpHelpers {

    private generateResultstUrl = "Home/GenerateResults";

    _selectedList: Array<Object>;
    errorMsg: string;
    
    constructor(private http: Http) {
        super(http);
        this._selectedList = null;
        this.errorMsg = "";
 
        this.getaction<Array<Object>>(this.generateResultstUrl).subscribe(
            result => {
               this._selectedList = result.Data;
            },
            error => this.errorMsg = error);
    }

    get hasError() {
        return this.errorMsg !== "";
    }

    get selectedList(): Array<Object> {
        return this._selectedList;
    }

    get hasSelectedList() {
        return this._selectedList !== null;
    }
}