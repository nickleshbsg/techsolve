import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { WhatsAppService } from '@core/services/whatsapp.service';
import { EnquiryService } from '@core/services/enquiry.service';

@Component({
  selector: 'ts-footer',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.scss'
})
export class FooterComponent {
  private wa = inject(WhatsAppService);
  private enquiry = inject(EnquiryService);
  year = new Date().getFullYear();

  openWhatsApp() { this.wa.openChat('footer'); }
  openEnquiry() { this.enquiry.openModal(); }
}
