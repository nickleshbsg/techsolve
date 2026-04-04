import { Component, inject } from '@angular/core';
import { WhatsAppService } from '@core/services/whatsapp.service';

@Component({
  selector: 'ts-whatsapp-button',
  standalone: true,
  templateUrl: './whatsapp-button.component.html',
  styleUrl: './whatsapp-button.component.scss'
})
export class WhatsappButtonComponent {
  private wa = inject(WhatsAppService);
  open() { this.wa.openChat('floating-button'); }
}
