import { Component, Output, EventEmitter } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'filter-textbox',
  standalone: true,
  imports: [FormsModule],
  template: `
    <form>
         Filter:
         <input type="text" name="filter"
                [(ngModel)]="model.filter" 
                (keyup)="filterChanged($event)"  />
    </form>
  `
})
export class FilterTextboxComponent {

  
    model: { filter: string } = { filter: '' };
    
    @Output()
    changed: EventEmitter<string> = new EventEmitter<string>();

    filterChanged(event: any) {
      event.preventDefault();
      this.changed.emit(this.model.filter); //Raise changed event
    }
}
