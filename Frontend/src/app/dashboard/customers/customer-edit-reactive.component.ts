import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, ActivatedRoute, RouterLink } from '@angular/router';
import { AbstractControl, FormBuilder, ReactiveFormsModule, FormGroup, Validators } from '@angular/forms';

import { CustomerDataService } from '../../services/customer-data.service';
import { ICustomer, IState } from '../../interfaces/customer.interface';
import { ValidationService } from '../../common/validation/validation.service';

@Component({
  selector: 'customer-edit-reactive',
  standalone: true,
  imports: [RouterLink, ReactiveFormsModule, CommonModule],
  templateUrl: './customer-edit-reactive.component.html',
  styleUrl: './customer-edit-reactive.component.scss'
})
export class CustomerEditReactiveComponent implements OnInit {

  customerForm: FormGroup = {} as FormGroup;
  get f(): { [key: string]: AbstractControl } {
    return this.customerForm.controls;
  }
  customer: ICustomer = {
    firstName: '',
    lastName: '',
    gender: '',
    address: '',
    email: '',
    city: '',
    stateId: 0,
    zip: 0
  };
  states: IState[] = [];
  errorMessage = '';
  deleteMessageEnabled: boolean = false;
  operationText: string = 'Insert';
  
  constructor(private router: Router, 
              private route: ActivatedRoute, 
              private customerDataService: CustomerDataService,
              private formBuilder: FormBuilder) { }

  ngOnInit() {
    let id = this.route.snapshot.params['id'];
    if (id !== '0') {
      this.operationText = 'Update';
      this.getCustomer(id);
    }

    this.getStates();
    this.buildForm();
  }

  getCustomer(id: string) {
      this.customerDataService.getCustomer(id)
        .subscribe((customer: ICustomer) => {
          this.customer = customer;
          this.buildForm();
        },
        (err) => console.log(err));
  }

  buildForm() {
      this.customerForm = this.formBuilder.group({
        firstName:  [this.customer.firstName, Validators.required],
        lastName:   [this.customer.lastName, Validators.required],
        gender:     [this.customer.gender, Validators.required],
        email:      [this.customer.email, [Validators.required, ValidationService.emailValidator]],
        address:    [this.customer.address, Validators.required],
        city:       [this.customer.city, Validators.required],
        stateId:    [this.customer.stateId, Validators.required]
      });
  }

  getStates() {
    this.customerDataService.getStates().subscribe((states: IState[]) => this.states = states);
  }
  
  submit({ value, valid }: { value: ICustomer, valid: boolean }) {
      
      value.id = this.customer.id;
      value.zip = this.customer.zip || 0; 
      // var customer: ICustomer = {
      //   id: this.customer.id,
      // };

      if (value.id) {

        this.customerDataService.updateCustomer(value)
          .subscribe((customer: ICustomer) => {
            if (customer) {
              this.router.navigate(['/customers']);
            }
            else {
              this.errorMessage = 'Unable to save customer';
            }
          },
          (err) => console.log(err));

      } else {

        this.customerDataService.insertCustomer(value)
          .subscribe((customer: ICustomer) => {
            if (customer) {
              this.router.navigate(['/customers']);
            }
            else {
              this.errorMessage = 'Unable to add customer';
            }
          },
          (err) => console.log(err));
          
      }
  }
  
  cancel(event: Event) {
    event.preventDefault();
    this.router.navigate(['/customers']);
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
