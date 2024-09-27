import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { firstValueFrom, Observable, of } from 'rxjs';

@Component({
  selector: 'app-test-page',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './test-page.component.html',
  styleUrl: './test-page.component.scss'
})
export class TestPageComponent implements OnInit {

  test1$: Observable<string> = new Observable(observer => observer.next('abc'));
  test2$: Observable<string> = of ('xyz');

  value1!: string;
  
  async ngOnInit() {
    this.value1 = await firstValueFrom(this.test1$, {defaultValue: 'Some default value'});
    alert(this.value1);
  }
}
