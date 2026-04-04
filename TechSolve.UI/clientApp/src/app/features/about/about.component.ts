import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { EnquiryService } from '@core/services/enquiry.service';
import { WhatsAppService } from '@core/services/whatsapp.service';

@Component({
  selector: 'ts-about',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './about.component.html',
  styleUrl: './about.component.scss'
})
export class AboutComponent {
  enquiry = inject(EnquiryService);
  private wa = inject(WhatsAppService);

  team = [
    { name: 'Aryan Sharma', role: 'Co-Founder & CEO', bio: '15 years in enterprise software. Ex-Infosys, ex-Wipro. Scaled 3 SaaS platforms to 1M+ users.', initials: 'AS', color: 'linear-gradient(135deg,#1a3ed4,#0a1fa8)' },
    { name: 'Priya Nair', role: 'CTO & Co-Founder', bio: 'Cloud architect certified in Azure & AWS. Led .NET migration projects for HDFC and Mahindra.', initials: 'PN', color: 'linear-gradient(135deg,#7c3aed,#5b21b6)' },
    { name: 'Karan Mehta', role: 'Head of AI & ML', bio: 'PhD in Machine Learning, IIT Bombay. Designed AI pipelines processing 50M+ documents per month.', initials: 'KM', color: 'linear-gradient(135deg,#0f766e,#065f46)' },
    { name: 'Sneha Patel', role: 'Head of Delivery', bio: 'PMP certified with 12 years leading enterprise deliveries. 98% on-time rate across 60+ projects.', initials: 'SP', color: 'linear-gradient(135deg,#b45309,#92400e)' },
    { name: 'Ravi Kumar', role: 'Lead Angular Architect', bio: 'Angular GDE (Google Developer Expert). Speaker at NgConf 2023. Core contributor to Angular libraries.', initials: 'RK', color: 'linear-gradient(135deg,#dc2626,#991b1b)' },
    { name: 'Divya Rao', role: 'Head of Cybersecurity', bio: 'CISSP & CEH certified. Led ISO 27001 implementations for 20+ financial institutions across India.', initials: 'DR', color: 'linear-gradient(135deg,#0ea5e9,#0369a1)' }
  ];

  values = [
    { icon: '🎯', title: 'Senior-only delivery', desc: 'Every engagement is staffed by engineers with 5+ years specialisation. No juniors, no hand-holding.' },
    { icon: '🔍', title: 'Radical transparency', desc: 'Live dashboards, weekly demos, and direct access to your engineers. No black boxes.' },
    { icon: '🛡️', title: 'Your IP, always yours', desc: 'Mutual NDAs from day one. Full IP transfer on final payment with no exceptions or hidden clauses.' },
    { icon: '🔧', title: 'We stay after go-live', desc: '3-month warranty on every project plus SLA-backed maintenance. We own the quality long-term.' },
    { icon: '📈', title: 'Outcome-focused', desc: 'We measure success by business impact — not tickets closed or lines of code written.' },
    { icon: '🤝', title: 'Partnership, not vendor', desc: 'We tell you when something is a bad idea. That\'s what partners do — not just vendors who bill hours.' }
  ];

  openWhatsApp() { this.wa.openChat('about'); }
}
