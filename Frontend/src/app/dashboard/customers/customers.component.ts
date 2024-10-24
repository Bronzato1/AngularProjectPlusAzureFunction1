import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { DataFilterService } from '../../services/data-filter.service';
import { CustomerDataService } from '../../services/customer-data.service';
import { ICustomer, IOrder, IPagedResults } from '../../interfaces/customer.interface';
import { FilterTextboxComponent } from '../../common/filter-textbox/filter-textbox.component';
import { CustomersGridComponent } from './customers-grid.component';
import { PaginationComponent } from '../../common/pagination/pagination.component';

@Component({ 
  selector: 'customer', 
  standalone: true,
  imports: [FilterTextboxComponent, RouterLink, CustomersGridComponent, PaginationComponent],
  templateUrl: './customers.component.html'
})
export class CustomersComponent implements OnInit {

  customers: ICustomer[] = [];
  filteredCustomers: ICustomer[] = [];

  totalRecords: number = 0;
  pageSize: number = 10;

  constructor(private router: Router, 
              private dataService: CustomerDataService,
              private dataFilter: DataFilterService) { }
  
  ngOnInit() {
    this.getCustomersPage(1);
  }

  filterChanged(filterText: string) {
    if (filterText && this.customers) {
        let props = ['firstName', 'lastName', 'address', 'city', 'state.name'];
        this.filteredCustomers = this.dataFilter.filter(this.customers, props, filterText);
    }
    else {
      this.filteredCustomers = this.customers;
    }
  }

  pageChanged(page: number) {
    this.getCustomersPage(page);
  }

  getCustomersPage(page: number) {
    this.dataService.getCustomersPage((page - 1) * this.pageSize, this.pageSize)
        .subscribe((response: IPagedResults<ICustomer[]>) => {
          this.customers = this.filteredCustomers = response.results;
          this.totalRecords = response.totalRecords;
        },
        (err: any) => console.log(err),
        () => console.log('getCustomersPage() retrieved customers'));
  }

}
