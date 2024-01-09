import { Component, OnInit } from '@angular/core';
import { InvoiceService } from '../invoice.service';
import { InvoiceHeader } from '../models/InvoiceHeader';
import { CreateInvoice } from '../models/CreateInvoice';

@Component({
  selector: 'app-invoice-header',
  templateUrl: './invoice-header.component.html',
  styleUrls: ['./invoice-header.component.css'],
})
export class InvoiceHeaderComponent implements OnInit {
  constructor(private service: InvoiceService) {}
  invoices: InvoiceHeader[] = [];
  createdInvoice: CreateInvoice = {
    customerName: '',
    cashierId: 0,
    branchId: 0,
  };
  items: any[] = [];
  ngOnInit(): void {
    this.service.selectedInvoice = {
      id: 0,
    };
    this.getAll();
  }

  getAll() {
    this.service.getAllInvoices().subscribe((res: any) => {
      this.invoices = res;
      console.log(this.invoices);
    });
  }

  getItems() {
    this.service
      .getItemsByHeaderID(this.service.selectedInvoice.id)
      .subscribe((res: any) => {
        this.service.invoicesDetails = res;
        this.items = this.service.invoicesDetails;
      });
  }

  fillData(item: InvoiceHeader) {
    this.service.selectedInvoice.id = item.id;
    this.service.selectedInvoice.customerName = item.customerName;
    this.service.selectedInvoice.invoicedate = item.invoicedate;
    this.service.selectedInvoice.invoiceDetails = [];
    this.getItems();
    console.log(this.service.selectedInvoice);
    console.log(this.service.invoicesDetails);
  }

  save() {
    console.log(this.createdInvoice);

    this.service.AddNewInvoice(this.createdInvoice).subscribe((res) => {
      this.getAll();
    });
  }
  deleteInvoice(id?: number) {
    this.service.DeleteInvoice(id).subscribe((res) => {
      this.getAll();
    });
  }
}
