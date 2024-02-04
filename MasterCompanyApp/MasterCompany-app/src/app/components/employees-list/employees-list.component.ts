import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MasterCompanyApi } from 'src/app/models/masterCompanyApi.model';
import { MasterCompanyApiService } from 'src/app/services/employees-list/master-company-api.service';

const ELEMENT_DATA: MasterCompanyApi[] = [];

@Component({
  selector: 'app-employees-list',
  templateUrl: './employees-list.component.html',
  styleUrls: ['./employees-list.component.css'],
})
export class EmployeesListComponent {
  displayedColumns: string[] = [
    'Name',
    'LastName',
    'Document',
    'Salary',
    'Gender',
    'Position',
    'StartDate',
  ];
  dataSource = new MatTableDataSource<MasterCompanyApi>(ELEMENT_DATA);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  
  constructor(private _masterCompanyService: MasterCompanyApiService) {}

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;

    this._masterCompanyService.getEmployees().subscribe((response) => {
      console.log(response);
      if (response.employees != null) {
        this.dataSource.data = response.employees;
      }
    });
  }



}
