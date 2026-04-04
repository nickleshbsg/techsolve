import { Injectable, inject, signal } from '@angular/core';
import { ApiService } from './api.service';
import { CreateEnquiryRequest } from '../models/enquiry.model';

@Injectable({ providedIn: 'root' })
export class EnquiryService {
  private api = inject(ApiService);

  prefilledService = signal<{ slug: string; name: string } | null>(null);
  modalOpen = signal(false);

  openModal(serviceSlug?: string, serviceName?: string) {
    if (serviceSlug && serviceName) {
      this.prefilledService.set({ slug: serviceSlug, name: serviceName });
    } else {
      this.prefilledService.set(null);
    }
    this.modalOpen.set(true);
  }

  closeModal() {
    this.modalOpen.set(false);
    this.prefilledService.set(null);
  }

  submit(payload: CreateEnquiryRequest) {
    return this.api.createEnquiry({
      ...payload,
      sourcePageUrl: window.location.href,
      sourcePageTitle: document.title
    });
  }
}
