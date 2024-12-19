import { Injectable } from '@angular/core';
import {MessageService} from "primeng/api";

@Injectable({
  providedIn: 'root',
})
export class NotificationService {

  constructor(private messageService: MessageService) { }

  public showSuccess(title: string, message: string): void {
    this.messageService.add({ severity: 'success', summary: title, detail: message })
  }

  public showInfo(title: string, message: string): void {
    this.messageService.add({ severity: 'info', summary: title, detail: message })
  }

  public showWarning(title: string, message: string): void {
    this.messageService.add({ severity: 'warn', summary: title, detail: message })
  }

  public showError(title: string, message: string): void {
    this.messageService.add({ severity: 'error', summary: title, detail: message })
  }
}
