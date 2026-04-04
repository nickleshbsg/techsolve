import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, ActivatedRoute } from '@angular/router';
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
  private api = inject(ApiService);
  private route = inject(ActivatedRoute);
  enquiry = inject(EnquiryService);
  private wa = inject(WhatsAppService);
  service = signal<ServiceDetail | null>(null);
  loading = signal(true);

  // Static SVG illustrations per service
  illustrations: Record<string, string> = {
    'custom-software-development': `<svg viewBox="0 0 340 260" fill="none" xmlns="http://www.w3.org/2000/svg">
      <rect width="340" height="260" rx="16" fill="#eef2ff"/>
      <rect x="20" y="20" width="200" height="140" rx="10" fill="white" stroke="#dbeafe" stroke-width="1.5"/>
      <rect x="20" y="20" width="200" height="32" rx="10" fill="#1a3ed4" opacity=".9"/>
      <circle cx="36" cy="36" r="5" fill="white" opacity=".6"/>
      <circle cx="50" cy="36" r="5" fill="white" opacity=".6"/>
      <circle cx="64" cy="36" r="5" fill="white" opacity=".6"/>
      <rect x="34" y="66" width="60" height="6" rx="3" fill="#818cf8" opacity=".8"/>
      <rect x="34" y="82" width="100" height="5" rx="2.5" fill="#c7d2fe" opacity=".8"/>
      <rect x="34" y="96" width="80" height="5" rx="2.5" fill="#e0e7ff" opacity=".9"/>
      <rect x="34" y="110" width="120" height="5" rx="2.5" fill="#c7d2fe" opacity=".7"/>
      <rect x="34" y="124" width="90" height="5" rx="2.5" fill="#e0e7ff" opacity=".9"/>
      <rect x="34" y="138" width="70" height="5" rx="2.5" fill="#818cf8" opacity=".5"/>
      <!-- Right floating cards -->
      <rect x="240" y="30" width="84" height="55" rx="10" fill="white" stroke="#dbeafe" stroke-width="1.5"/>
      <circle cx="262" cy="52" r="12" fill="#eef2ff"/>
      <text x="257" y="57" fill="#1a3ed4" font-size="12">⚙️</text>
      <rect x="252" y="68" width="52" height="5" rx="2.5" fill="#c7d2fe"/>
      <rect x="240" y="100" width="84" height="55" rx="10" fill="white" stroke="#dbeafe" stroke-width="1.5"/>
      <circle cx="262" cy="122" r="12" fill="#eef2ff"/>
      <text x="257" y="127" fill="#1a3ed4" font-size="12">🚀</text>
      <rect x="252" y="138" width="52" height="5" rx="2.5" fill="#c7d2fe"/>
      <!-- Bottom badges -->
      <rect x="20" y="180" width="110" height="34" rx="10" fill="#1a3ed4"/>
      <text x="34" y="202" fill="white" font-size="11" font-weight="700">✓ On-time Delivery</text>
      <rect x="144" y="180" width="90" height="34" rx="10" fill="#eef2ff" stroke="#c7d2fe" stroke-width="1.5"/>
      <text x="158" y="202" fill="#1a3ed4" font-size="11" font-weight="700">NDA Signed</text>
      <rect x="248" y="180" width="76" height="34" rx="10" fill="#f0fdf4" stroke="#bbf7d0" stroke-width="1.5"/>
      <text x="262" y="202" fill="#16a34a" font-size="11" font-weight="700">IP Yours</text>
    </svg>`,
    'web-application-development': `<svg viewBox="0 0 340 260" fill="none" xmlns="http://www.w3.org/2000/svg">
      <rect width="340" height="260" rx="16" fill="#eef2ff"/>
      <!-- Browser -->
      <rect x="20" y="20" width="300" height="200" rx="12" fill="white" stroke="#dbeafe" stroke-width="1.5"/>
      <rect x="20" y="20" width="300" height="36" rx="12" fill="#f8faff"/>
      <rect x="20" y="44" width="300" height="12" fill="#f8faff"/>
      <circle cx="40" cy="38" r="5" fill="#ff5f57" opacity=".8"/>
      <circle cx="56" cy="38" r="5" fill="#febc2e" opacity=".8"/>
      <circle cx="72" cy="38" r="5" fill="#28c840" opacity=".8"/>
      <rect x="90" y="30" width="160" height="16" rx="8" fill="#eef2ff"/>
      <text x="140" y="42" fill="#6366f1" font-size="8" font-family="monospace">techsolve.in</text>
      <!-- Nav bar in page -->
      <rect x="30" y="68" width="280" height="24" rx="6" fill="#1a3ed4"/>
      <rect x="38" y="74" width="40" height="12" rx="4" fill="rgba(255,255,255,.3)"/>
      <rect x="90" y="77" width="25" height="6" rx="3" fill="rgba(255,255,255,.5)"/>
      <rect x="122" y="77" width="25" height="6" rx="3" fill="rgba(255,255,255,.5)"/>
      <rect x="154" y="77" width="25" height="6" rx="3" fill="rgba(255,255,255,.5)"/>
      <rect x="258" y="73" width="44" height="14" rx="7" fill="white" opacity=".9"/>
      <!-- Hero area in page -->
      <rect x="30" y="102" width="160" height="10" rx="5" fill="#1a3ed4" opacity=".8"/>
      <rect x="30" y="118" width="120" height="7" rx="3.5" fill="#c7d2fe"/>
      <rect x="30" y="132" width="80" height="7" rx="3.5" fill="#c7d2fe" opacity=".6"/>
      <rect x="30" y="150" width="60" height="22" rx="8" fill="#1a3ed4"/>
      <text x="40" y="165" fill="white" font-size="8" font-weight="700">Enquire →</text>
      <!-- Right image placeholder -->
      <rect x="210" y="100" width="100" height="80" rx="10" fill="linear-gradient(135deg,#dbeafe,#e0e7ff)" stroke="#c7d2fe" stroke-width="1"/>
      <rect x="210" y="100" width="100" height="80" rx="10" fill="#dbeafe"/>
      <circle cx="260" cy="130" r="18" fill="#c7d2fe"/>
      <path d="M252 130 l8-8 8 8" stroke="#1a3ed4" stroke-width="2" fill="none"/>
      <path d="M260 122 v16" stroke="#1a3ed4" stroke-width="2"/>
      <!-- Stats row -->
      <rect x="30" y="190" width="60" height="20" rx="6" fill="#eef2ff"/>
      <text x="42" y="204" fill="#1a3ed4" font-size="8" font-weight="700">150+ Apps</text>
      <rect x="100" y="190" width="60" height="20" rx="6" fill="#eef2ff"/>
      <text x="112" y="204" fill="#1a3ed4" font-size="8" font-weight="700">98% SLA</text>
      <rect x="170" y="190" width="60" height="20" rx="6" fill="#f0fdf4"/>
      <text x="182" y="204" fill="#16a34a" font-size="8" font-weight="700">A+ Perf</text>
    </svg>`,
    'ai-machine-learning': `<svg viewBox="0 0 340 260" fill="none" xmlns="http://www.w3.org/2000/svg">
      <rect width="340" height="260" rx="16" fill="#eef2ff"/>
      <!-- Neural network nodes -->
      <circle cx="60" cy="80" r="18" fill="white" stroke="#818cf8" stroke-width="2"/>
      <circle cx="60" cy="130" r="18" fill="white" stroke="#818cf8" stroke-width="2"/>
      <circle cx="60" cy="180" r="18" fill="white" stroke="#818cf8" stroke-width="2"/>
      <circle cx="170" cy="60" r="20" fill="#1a3ed4" opacity=".9"/>
      <circle cx="170" cy="110" r="20" fill="#1a3ed4"/>
      <circle cx="170" cy="160" r="20" fill="#1a3ed4" opacity=".9"/>
      <circle cx="170" cy="210" r="20" fill="#1a3ed4" opacity=".7"/>
      <circle cx="280" cy="90" r="22" fill="#818cf8" opacity=".9"/>
      <circle cx="280" cy="170" r="22" fill="#818cf8"/>
      <!-- Connection lines -->
      <line x1="78" y1="80" x2="150" y2="66" stroke="#c7d2fe" stroke-width="1.5"/>
      <line x1="78" y1="80" x2="150" y2="112" stroke="#c7d2fe" stroke-width="1.5"/>
      <line x1="78" y1="130" x2="150" y2="112" stroke="#c7d2fe" stroke-width="1.5"/>
      <line x1="78" y1="130" x2="150" y2="160" stroke="#c7d2fe" stroke-width="1.5"/>
      <line x1="78" y1="180" x2="150" y2="160" stroke="#c7d2fe" stroke-width="1.5"/>
      <line x1="78" y1="180" x2="150" y2="212" stroke="#c7d2fe" stroke-width="1.5"/>
      <line x1="190" y1="66" x2="258" y2="94" stroke="#818cf8" stroke-width="1.5" opacity=".6"/>
      <line x1="190" y1="112" x2="258" y2="94" stroke="#818cf8" stroke-width="1.5" opacity=".6"/>
      <line x1="190" y1="112" x2="258" y2="172" stroke="#818cf8" stroke-width="1.5" opacity=".6"/>
      <line x1="190" y1="160" x2="258" y2="172" stroke="#818cf8" stroke-width="1.5" opacity=".6"/>
      <line x1="190" y1="212" x2="258" y2="172" stroke="#818cf8" stroke-width="1.5" opacity=".6"/>
      <!-- Labels in nodes -->
      <text x="55" y="84" fill="#1a3ed4" font-size="9" font-weight="700">IN</text>
      <text x="55" y="134" fill="#1a3ed4" font-size="9" font-weight="700">IN</text>
      <text x="55" y="184" fill="#1a3ed4" font-size="9" font-weight="700">IN</text>
      <text x="163" y="65" fill="white" font-size="9" font-weight="700">H1</text>
      <text x="163" y="115" fill="white" font-size="9" font-weight="700">H2</text>
      <text x="163" y="165" fill="white" font-size="9" font-weight="700">H3</text>
      <text x="163" y="215" fill="white" font-size="9" font-weight="700">H4</text>
      <text x="271" y="95" fill="white" font-size="9" font-weight="700">OUT</text>
      <text x="271" y="175" fill="white" font-size="9" font-weight="700">OUT</text>
      <!-- Accuracy badge -->
      <rect x="210" y="20" width="110" height="36" rx="10" fill="white" stroke="#dbeafe" stroke-width="1.5"/>
      <text x="224" y="34" fill="#16a34a" font-size="10" font-weight="700">✓ 99.2% Accuracy</text>
      <text x="228" y="48" fill="#6b7280" font-size="8">10k docs / day</text>
    </svg>`,
    'cloud-devops': `<svg viewBox="0 0 340 260" fill="none" xmlns="http://www.w3.org/2000/svg">
      <rect width="340" height="260" rx="16" fill="#eef2ff"/>
      <!-- Cloud shape -->
      <ellipse cx="170" cy="100" rx="90" ry="50" fill="white" stroke="#c7d2fe" stroke-width="2"/>
      <ellipse cx="120" cy="115" rx="45" ry="30" fill="white" stroke="#c7d2fe" stroke-width="2"/>
      <ellipse cx="220" cy="115" rx="45" ry="30" fill="white" stroke="#c7d2fe" stroke-width="2"/>
      <!-- Cloud inner icons -->
      <circle cx="140" cy="100" r="14" fill="#eef2ff"/>
      <text x="133" y="105" font-size="12">☁️</text>
      <circle cx="170" cy="90" r="14" fill="#eef2ff"/>
      <text x="163" y="95" font-size="12">⚡</text>
      <circle cx="200" cy="100" r="14" fill="#eef2ff"/>
      <text x="193" y="105" font-size="12">🔒</text>
      <!-- Connection lines down -->
      <line x1="120" y1="145" x2="80" y2="185" stroke="#1a3ed4" stroke-width="2" stroke-dasharray="4 3" opacity=".6"/>
      <line x1="170" y1="150" x2="170" y2="185" stroke="#1a3ed4" stroke-width="2" stroke-dasharray="4 3" opacity=".6"/>
      <line x1="220" y1="145" x2="260" y2="185" stroke="#1a3ed4" stroke-width="2" stroke-dasharray="4 3" opacity=".6"/>
      <!-- Server boxes -->
      <rect x="50" y="185" width="60" height="40" rx="8" fill="#1a3ed4"/>
      <text x="63" y="200" fill="white" font-size="8" font-weight="700">Azure</text>
      <rect x="55" y="205" width="50" height="3" rx="1.5" fill="rgba(255,255,255,.4)"/>
      <rect x="55" y="211" width="35" height="3" rx="1.5" fill="rgba(255,255,255,.4)"/>
      <rect x="140" y="185" width="60" height="40" rx="8" fill="#818cf8"/>
      <text x="155" y="200" fill="white" font-size="8" font-weight="700">AWS</text>
      <rect x="145" y="205" width="50" height="3" rx="1.5" fill="rgba(255,255,255,.4)"/>
      <rect x="145" y="211" width="35" height="3" rx="1.5" fill="rgba(255,255,255,.4)"/>
      <rect x="230" y="185" width="60" height="40" rx="8" fill="#0f766e"/>
      <text x="244" y="200" fill="white" font-size="8" font-weight="700">K8s</text>
      <rect x="235" y="205" width="50" height="3" rx="1.5" fill="rgba(255,255,255,.4)"/>
      <rect x="235" y="211" width="35" height="3" rx="1.5" fill="rgba(255,255,255,.4)"/>
      <!-- Uptime badge -->
      <rect x="110" y="230" width="120" height="24" rx="8" fill="#f0fdf4" stroke="#bbf7d0" stroke-width="1.5"/>
      <text x="132" y="246" fill="#16a34a" font-size="10" font-weight="700">✓ 99.99% Uptime SLA</text>
    </svg>`
  };

  getIllustration(): string {
    const slug = this.service()?.slug ?? '';
    return this.illustrations[slug] ?? this.illustrations['custom-software-development'];
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
