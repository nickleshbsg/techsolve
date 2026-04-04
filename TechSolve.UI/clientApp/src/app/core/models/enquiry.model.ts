export interface CreateEnquiryRequest {
  fullName: string;
  email: string;
  phone?: string;
  company?: string;
  serviceSlug: string;
  serviceName: string;
  message: string;
  sourcePageUrl: string;
  sourcePageTitle?: string;
}

export interface EnquiryResponse {
  id: number;
  ticketId: string;
  fullName: string;
  email: string;
  serviceName: string;
  status: number;
  createdAt: string;
}
