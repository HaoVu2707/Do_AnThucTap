import { Component, Input, Output } from '@angular/core';

@Component({
  selector: 'ngx-status-card',
  styleUrls: ['./status-card.component.scss'],
  template: `
    <nb-card (click)="on = !on" [ngClass]="{'off': !on}">
      <div class="icon-container">
        <div class="icon">
          <img [src] = "type">
          <ng-content></ng-content>
        </div>
      </div>
      <div class="details">
        <div class="title">{{ title }}</div>
        <div >
          <div class= "title promotioned">{{price-discount}} </div>
          <div class= "inline">{{price}} </div>
        </div>
        <div class="status">{{ on ? 'ON' : 'OFF' }}</div>
      </div>

    </nb-card>

  `,
})
export class StatusCardComponent {

 @Input() price: number;
 @Input() discount: number;
  @Input() title: string;
  @Input() type: string;
  @Input() on = true;
  @Output() quantity : number;
}
