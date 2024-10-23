import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { CustomerDataService } from '../../services/customer-data.service';
import { ICustomer, IState } from '../../interfaces/customer.interface';

@Component({
  selector: 'customer-edit',
  templateUrl: './customer-edit.component.html'
})
export class CustomerEditComponent implements OnInit {

  customer: ICustomer = {
    firstName: '',
    lastName: '',
    gender: '',
    address: '',
    email: '',
    city: '',
    zip: 0
  };
  states: IState[] = [];
  errorMessage = '';
  deleteMessageEnabled = false;
  operationText = 'Insert';
  
  constructor(private router: Router, 
              private route: ActivatedRoute, 
              private customerDataService: CustomerDataService) { }

  ngOnInit() {
    let id = this.route.snapshot.params['id'];
    if (id !== '0') {
      this.operationText = 'Update';
      this.getCustomer(id);
    }

    this.getStates();
  }

  getCustomer(id: string) {
      this.customerDataService.getCustomer(id)
        .subscribe((customer: ICustomer) => {
          this.customer = customer;
        },
        (err: any) => console.log(err));
  }

  getStates() {
    this.customerDataService.getStates().subscribe((states: IState[]) => this.states = states);
  }
  
  submit() {

      if (this.customer.id) {

        this.customerDataService.updateCustomer(this.customer)
          .subscribe((customer: ICustomer) => {
            if (customer) {
              this.router.navigate(['/customers']);
            } else {
              this.errorMessage = 'Unable to save customer';
            }
          },
          (err: any) => console.log(err));

      } else {

        this.customerDataService.insertCustomer(this.customer)
          .subscribe((customer: ICustomer) => {
            if (customer) {
              this.router.navigate(['/customers']);
            }
            else {
              this.errorMessage = 'Unable to add customer';
            }
          },
          (err: any) => console.log(err));
          
      }
  }
  
  cancel(event: Event) {
    event.preventDefault();
    this.router.navigate(['/']);
  }

  delete(event: Event) {
    event.preventDefault();
    this.customerDataService.deleteCustomer(this.customer.id as string)
        .subscribe((status: boolean) => {
          if (status) {
            this.router.navigate(['/customers']);
          }
          else {
            this.errorMessage = 'Unable to delete customer';
          }
        },
        (err) => console.log(err));
  }

}
