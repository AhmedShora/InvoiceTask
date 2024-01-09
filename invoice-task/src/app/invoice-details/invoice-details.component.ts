import { Component, Input, OnInit } from '@angular/core';
import { InvoiceService } from '../invoice.service';
import { InvoiceDetail } from '../models/InvoiceDetails';
import { InvoiceHeaderComponent } from '../invoice-header/invoice-header.component';

@Component({
  selector: 'app-invoice-details',
  templateUrl: './invoice-details.component.html',
  styleUrls: ['./invoice-details.component.css'],
})
export class InvoiceDetailsComponent implements OnInit {
  @Input() data: any[] =[] ;
  constructor() {}

 
  ngOnInit(): void {
   
  }

}
