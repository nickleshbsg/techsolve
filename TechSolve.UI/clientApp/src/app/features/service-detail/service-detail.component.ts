import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, ActivatedRoute } from '@angular/router';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { ApiService } from '@core/services/api.service';
import { EnquiryService } from '@core/services/enquiry.service';
import { WhatsAppService } from '@core/services/whatsapp.service';
import { ServiceDetail } from '@core/models/service.model';

@Component({
  selector: 'ts-service-detail',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './service-detail.component.html',
  styleUrl: './service-detail.component.scss'
})
export class ServiceDetailComponent implements OnInit {
  private api      = inject(ApiService);
  private route    = inject(ActivatedRoute);
  private sanitizer = inject(DomSanitizer);
  enquiry = inject(EnquiryService);
  private wa = inject(WhatsAppService);

  service = signal<ServiceDetail | null>(null);
  loading = signal(true);

  private svgMap: Record<string, string> = {
    'custom-software-development': `<svg viewBox="0 0 340 260" fill="none" xmlns="http://www.w3.org/2000/svg"><rect width="340" height="260" rx="16" fill="#eef2ff"/><rect x="20" y="20" width="200" height="140" rx="10" fill="white" stroke="#dbeafe" stroke-width="1.5"/><rect x="20" y="20" width="200" height="32" rx="10" fill="#1a3ed4" opacity=".9"/><circle cx="36" cy="36" r="5" fill="white" opacity=".6"/><circle cx="50" cy="36" r="5" fill="white" opacity=".6"/><circle cx="64" cy="36" r="5" fill="white" opacity=".6"/><rect x="34" y="66" width="60" height="6" rx="3" fill="#818cf8" opacity=".8"/><rect x="34" y="82" width="100" height="5" rx="2.5" fill="#c7d2fe" opacity=".8"/><rect x="34" y="96" width="80" height="5" rx="2.5" fill="#e0e7ff" opacity=".9"/><rect x="34" y="110" width="120" height="5" rx="2.5" fill="#c7d2fe" opacity=".7"/><rect x="240" y="30" width="84" height="55" rx="10" fill="white" stroke="#dbeafe" stroke-width="1.5"/><circle cx="262" cy="52" r="12" fill="#eef2ff"/><text x="257" y="57" fill="#1a3ed4" font-size="12">⚙️</text><rect x="252" y="68" width="52" height="5" rx="2.5" fill="#c7d2fe"/><rect x="20" y="180" width="110" height="34" rx="10" fill="#1a3ed4"/><text x="34" y="202" fill="white" font-size="11" font-weight="700">✓ On-time Delivery</text><rect x="144" y="180" width="90" height="34" rx="10" fill="#eef2ff" stroke="#c7d2fe" stroke-width="1.5"/><text x="158" y="202" fill="#1a3ed4" font-size="11" font-weight="700">NDA Signed</text></svg>`,
    'web-application-development': `<svg viewBox="0 0 340 260" fill="none" xmlns="http://www.w3.org/2000/svg"><rect width="340" height="260" rx="16" fill="#eef2ff"/><rect x="20" y="20" width="300" height="200" rx="12" fill="white" stroke="#dbeafe" stroke-width="1.5"/><rect x="20" y="20" width="300" height="36" rx="12" fill="#f8faff"/><rect x="20" y="44" width="300" height="12" fill="#f8faff"/><circle cx="40" cy="38" r="5" fill="#ff5f57" opacity=".8"/><circle cx="56" cy="38" r="5" fill="#febc2e" opacity=".8"/><circle cx="72" cy="38" r="5" fill="#28c840" opacity=".8"/><rect x="90" y="30" width="160" height="16" rx="8" fill="#eef2ff"/><rect x="30" y="68" width="280" height="24" rx="6" fill="#1a3ed4"/><rect x="30" y="102" width="160" height="10" rx="5" fill="#1a3ed4" opacity=".8"/><rect x="30" y="118" width="120" height="7" rx="3.5" fill="#c7d2fe"/><rect x="30" y="150" width="60" height="22" rx="8" fill="#1a3ed4"/><text x="40" y="165" fill="white" font-size="8" font-weight="700">Enquire</text></svg>`,
    'ai-machine-learning': `<svg viewBox="0 0 340 260" fill="none" xmlns="http://www.w3.org/2000/svg"><rect width="340" height="260" rx="16" fill="#eef2ff"/><circle cx="60" cy="80" r="18" fill="white" stroke="#818cf8" stroke-width="2"/><circle cx="60" cy="130" r="18" fill="white" stroke="#818cf8" stroke-width="2"/><circle cx="60" cy="180" r="18" fill="white" stroke="#818cf8" stroke-width="2"/><circle cx="170" cy="60" r="20" fill="#1a3ed4" opacity=".9"/><circle cx="170" cy="110" r="20" fill="#1a3ed4"/><circle cx="170" cy="160" r="20" fill="#1a3ed4" opacity=".9"/><circle cx="280" cy="90" r="22" fill="#818cf8" opacity=".9"/><circle cx="280" cy="170" r="22" fill="#818cf8"/><line x1="78" y1="80" x2="150" y2="66" stroke="#c7d2fe" stroke-width="1.5"/><line x1="78" y1="130" x2="150" y2="112" stroke="#c7d2fe" stroke-width="1.5"/><line x1="78" y1="180" x2="150" y2="160" stroke="#c7d2fe" stroke-width="1.5"/></svg>`,
    'cloud-devops': `<svg viewBox="0 0 340 260" fill="none" xmlns="http://www.w3.org/2000/svg"><rect width="340" height="260" rx="16" fill="#eef2ff"/><ellipse cx="170" cy="100" rx="90" ry="50" fill="white" stroke="#c7d2fe" stroke-width="2"/><ellipse cx="120" cy="115" rx="45" ry="30" fill="white" stroke="#c7d2fe" stroke-width="2"/><ellipse cx="220" cy="115" rx="45" ry="30" fill="white" stroke="#c7d2fe" stroke-width="2"/><rect x="50" y="185" width="60" height="40" rx="8" fill="#1a3ed4"/><text x="63" y="200" fill="white" font-size="8" font-weight="700">Azure</text><rect x="140" y="185" width="60" height="40" rx="8" fill="#818cf8"/><text x="155" y="200" fill="white" font-size="8" font-weight="700">AWS</text><rect x="230" y="185" width="60" height="40" rx="8" fill="#0f766e"/><text x="244" y="200" fill="white" font-size="8" font-weight="700">K8s</text></svg>`
  };

  getIllustration(): SafeHtml {
    const slug = this.service()?.slug ?? '';
    const svg = this.svgMap[slug] ?? this.svgMap['custom-software-development'];
    return this.sanitizer.bypassSecurityTrustHtml(svg);
  }

  ngOnInit() {
    const slug = this.route.snapshot.paramMap.get('slug') ?? '';
    this.api.getServiceBySlug(slug).subscribe({
      next: s => { this.service.set(s); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }

  openEnquiry() {
    const s = this.service();
    if (s) this.enquiry.openModal(s.slug, s.title);
  }
  openWhatsApp() {
    const s = this.service();
    this.wa.openChat('service-detail', s?.slug, s?.title);
  }
}
