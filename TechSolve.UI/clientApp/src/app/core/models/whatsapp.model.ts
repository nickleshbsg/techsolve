export interface TrackWhatsAppRequest {
  sessionId: string;
  pageUrl: string;
  pageTitle?: string;
  serviceSlug?: string;
  serviceName?: string;
  referrer?: string;
  clickSource: string;
}
