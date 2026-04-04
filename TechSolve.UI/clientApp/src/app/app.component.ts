import { Component, OnInit, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './shared/components/navbar/navbar.component';
import { FooterComponent } from './shared/components/footer/footer.component';
import { WhatsappButtonComponent } from './shared/components/whatsapp-button/whatsapp-button.component';
import { EnquiryModalComponent } from './shared/components/enquiry-modal/enquiry-modal.component';
import { WhatsAppService } from './core/services/whatsapp.service';

@Component({
  selector: 'ts-root',
  standalone: true,
  imports: [RouterOutlet, NavbarComponent, FooterComponent, WhatsappButtonComponent, EnquiryModalComponent],
  template: `
    <ts-navbar />
    <main>
      <router-outlet />
    </main>
    <ts-footer />
    <ts-whatsapp-button />
    <ts-enquiry-modal />
  `
})
export class AppComponent implements OnInit {
  private wa = inject(WhatsAppService);
  ngOnInit() { this.wa.init(); }
}
