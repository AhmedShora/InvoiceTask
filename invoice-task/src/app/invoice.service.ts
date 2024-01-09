import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { InvoiceHeader } from './models/InvoiceHeader';
import { InvoiceDetail } from './models/InvoiceDetails';
import { CreateInvoice } from './models/CreateInvoice';
@Injectable({
  providedIn: 'root',
})
export class InvoiceService {
  constructor(private http: HttpClient) {}

  selectedInvoice!: InvoiceHeader;

  invoicesDetails: InvoiceDetail[] = [];
  //selectedItem!: InvoiceDetail;

  getAllInvoices() {
    return this.http.get(environment.baseUrl + 'Invoice');
  }
  getAllDetails() {
    return this.http.get(environment.baseUrl + 'InvoiceItems');
  }

  getItemsByHeaderID(id?: Number) {
    return this.http.get(environment.baseUrl + 'InvoiceItems/items/' + id);
    //https://localhost:7030/api/InvoiceItems/items/2
  }

  AddNewInvoice(createdInvoice: CreateInvoice) {
    return this.http.post(environment.baseUrl + 'Invoice', createdInvoice);
  }

  DeleteInvoice(id?: number) {
    return this.http.delete(environment.baseUrl + 'Invoice/' + id);
  }
}
