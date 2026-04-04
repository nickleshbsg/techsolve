import { Injectable, inject } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs';
import { environment } from '@env/environment';
import { ApiService } from './api.service';

@Injectable({ providedIn: 'root' })
export class WhatsAppService {
  private api = inject(ApiService);
  private router = inject(Router);
  private sessionId = crypto.randomUUID();
  private currentUrl = '';
  private currentTitle = '';

  init() {
    this.router.events.pipe(filter(e => e instanceof NavigationEnd)).subscribe((e: any) => {
      this.currentUrl = window.location.href;
      this.currentTitle = document.title;
    });
  }

  openChat(source: string, serviceSlug?: string, serviceName?: string) {
    const msg = encodeURIComponent(
      `Hi TechSolve! I'm interested in discussing a project.\n\nPage: ${this.currentUrl}\nSource: ${source}`
    );
    window.open(`https://wa.me/${environment.whatsappNumber}?text=${msg}`, '_blank');

    this.api.trackWhatsApp({
      sessionId: this.sessionId,
      pageUrl: this.currentUrl,
      pageTitle: this.currentTitle,
      serviceSlug,
      serviceName,
      referrer: document.referrer,
      clickSource: source
    }).subscribe({ error: () => {} });
  }
}
