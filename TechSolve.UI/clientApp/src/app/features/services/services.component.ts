import { Component, OnInit, inject, signal } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterLink } from "@angular/router";
import { ApiService } from "@core/services/api.service";
import { EnquiryService } from "@core/services/enquiry.service";
import { ServiceSummary } from "@core/models/service.model";

@Component({
  selector: "ts-services",
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: "./services.component.html",
  styleUrl: "./services.component.scss",
})
export class ServicesComponent implements OnInit {
  private api = inject(ApiService);
  enquiry = inject(EnquiryService);
  services = signal<ServiceSummary[]>([]);

  models = [
    {
      icon: "💎",
      name: "Fixed Price",
      desc: "Best for well-defined projects with clear scope and deliverables.",
      features: [
        "Agreed scope & timeline upfront",
        "Predictable budget",
        "Milestone-based payments",
        "Best for MVP builds",
      ],
    },
    {
      icon: "⏱️",
      name: "Time & Materials",
      desc: "Ideal for evolving products where requirements develop iteratively.",
      features: [
        "Flexible scope adaptation",
        "Weekly sprint billing",
        "Full visibility on hours",
        "Best for complex platforms",
      ],
    },
    {
      icon: "👥",
      name: "Dedicated Team",
      desc: "Your own embedded engineering squad, fully integrated into your workflow.",
      features: [
        "Monthly team retainer",
        "Full-time senior engineers",
        "Integrates with Jira/Slack",
        "Scale up or down monthly",
      ],
    },
  ];

  ngOnInit() {
    this.api.getServices().subscribe((s) => {
      console.log(s);
      this.services.set(s);
    });
  }
  enquireService(s: ServiceSummary) {
    this.enquiry.openModal(s.slug, s.title);
  }
}
