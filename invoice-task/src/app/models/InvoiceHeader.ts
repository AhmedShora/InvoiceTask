import { InvoiceDetail } from "./InvoiceDetails";


export class InvoiceHeader {
  id?: number;
  customerName?: string;
  invoicedate?: Date;
  invoiceDetails?: InvoiceDetail[];
}
