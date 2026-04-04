import { Component, inject, HostListener, signal } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { WhatsAppService } from '@core/services/whatsapp.service';
import { EnquiryService } from '@core/services/enquiry.service';

@Component({
  selector: 'ts-navbar',
  standalone: true,
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
  private wa = inject(WhatsAppService);
  private enquiry = inject(EnquiryService);
  scrolled = signal(false);

  @HostListener('window:scroll')
  onScroll() { this.scrolled.set(window.scrollY > 20); }

  openWhatsApp() { this.wa.openChat('navbar'); }
  openEnquiry() { this.enquiry.openModal(); }
}
