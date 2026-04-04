import { Component, OnInit, inject, signal } from "@angular/core";
import { CommonModule, DatePipe } from "@angular/common";
import { RouterLink } from "@angular/router";
import { ApiService } from "@core/services/api.service";
import { WhatsAppService } from "@core/services/whatsapp.service";
import { EnquiryService } from "@core/services/enquiry.service";
import { ServiceSummary } from "@core/models/service.model";
import { BlogSummary } from "@core/models/blog.model";

@Component({
  selector: "ts-home",
  standalone: true,
  imports: [CommonModule, RouterLink, DatePipe],
  templateUrl: "./home.component.html",
  styleUrl: "./home.component.scss",
})
export class HomeComponent implements OnInit {
  private api = inject(ApiService);
  private wa = inject(WhatsAppService);
  enquiry = inject(EnquiryService);

  services = signal<ServiceSummary[]>([]);
  blogs = signal<BlogSummary[]>([]);
  openFaq = signal<number | null>(0);

  stats = [
    { num: "150+", label: "Projects Delivered", trend: "↑ 24 this year" },
    { num: "50+", label: "Senior Engineers", trend: "Avg. 7yr exp" },
    { num: "98%", label: "Client Satisfaction", trend: "↑ from 96%" },
    { num: "12yr", label: "Industry Experience", trend: "Est. 2012" },
  ];

  testimonials = [
    {
      text: "TechSolve rebuilt our core banking platform in 8 months. Their Angular & .NET Core architecture handles 200,000+ daily users without a single incident. Genuinely exceptional work.",
      name: "Rajiv Mehta",
      role: "CTO, FinanceFirst Ltd · Mumbai",
      initials: "RM",
      color: "linear-gradient(135deg,#1a3ed4,#0e2899)",
    },
    {
      text: "Their AI pipeline cut our document processing time by 78% and error rate by 94%. The team's proactiveness and technical honesty set them apart from every vendor we've worked with.",
      name: "Sunita Kapoor",
      role: "VP Engineering, LogiTrack India",
      initials: "SK",
      color: "linear-gradient(135deg,#0f766e,#065f46)",
    },
    {
      text: "POC to production in 14 weeks, zero surprises, complete visibility throughout. TechSolve is the only partner we'll consider for our next three product launches.",
      name: "Arjun Nair",
      role: "CEO, DataEdge Solutions · Delhi",
      initials: "AN",
      color: "linear-gradient(135deg,#7c3aed,#5b21b6)",
    },
  ];

  faqs = [
    {
      q: "How do you price projects?",
      a: "We offer Fixed-price, Time & Materials, and Dedicated Team models. We recommend the best fit after a free discovery call.",
    },
    {
      q: "How long does a typical project take?",
      a: "MVPs take 8–14 weeks. Enterprise platforms 4–12 months. We provide a milestone-based plan before work begins.",
    },
    {
      q: "What technologies do you specialise in?",
      a: "Angular 19, .NET Core 9, Azure/AWS, SQL Server/PostgreSQL. Also React, Python, AI/ML frameworks, Docker, Kubernetes.",
    },
    {
      q: "Do you sign NDAs?",
      a: "Yes — mutual NDAs before any discussion. Full IP transfers to you on final payment.",
    },
    {
      q: "Do you offer post-launch support?",
      a: "3-month bug-fix warranty on all projects, plus SLA-backed maintenance retainers with 4h–24h response times.",
    },
    {
      q: "Can you work with our in-house team?",
      a: "Absolutely. Staff augmentation is one of our most popular engagements. We integrate into your Jira, GitHub, Slack seamlessly.",
    },
  ];

  ngOnInit() {
    this.api.getServices().subscribe((s) => this.services.set(s));
    this.api.getBlogPosts(1, 3).subscribe((r) => this.blogs.set(r.items));
  }

  openWhatsApp(source: string) {
    this.wa.openChat(source);
  }
  toggleFaq(i: number) {
    this.openFaq.set(this.openFaq() === i ? null : i);
  }
  enquireService(s: ServiceSummary) {
    this.enquiry.openModal(s.slug, s.title);
  }
}
