import { Injectable, inject } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable, map } from "rxjs";
import { environment } from "@env/environment";
import { ApiResponse, PagedResponse } from "../models/api-response.model";
import { ServiceSummary, ServiceDetail } from "../models/service.model";
import { BlogSummary, BlogDetail } from "../models/blog.model";
import { CreateEnquiryRequest, EnquiryResponse } from "../models/enquiry.model";
import { TrackWhatsAppRequest } from "../models/whatsapp.model";

@Injectable({ providedIn: "root" })
export class ApiService {
  private http = inject(HttpClient);
  private base = environment.apiUrl;

  // ── Services ─────────────────────────────────────────────────────
  getServices(): Observable<ServiceSummary[]> {
    return this.http
      .get<ApiResponse<ServiceSummary[]>>(`${this.base}/services`)
      .pipe(map((r) => r.data));
  }

  getServiceBySlug(slug: string): Observable<ServiceDetail> {
    return this.http
      .get<ApiResponse<ServiceDetail>>(`${this.base}/services/${slug}`)
      .pipe(map((r) => r.data));
  }

  // ── Blog ─────────────────────────────────────────────────────────
  getBlogPosts(
    page = 1,
    pageSize = 9,
    category?: string
  ): Observable<PagedResponse<BlogSummary>> {
    let params = new HttpParams().set("page", page).set("pageSize", pageSize);
    if (category) params = params.set("category", category);
    return this.http
      .get<ApiResponse<PagedResponse<BlogSummary>>>(`${this.base}/blog`, {
        params,
      })
      .pipe(map((r) => r.data));
  }

  getBlogBySlug(slug: string): Observable<BlogDetail> {
    return this.http
      .get<ApiResponse<BlogDetail>>(`${this.base}/blog/${slug}`)
      .pipe(map((r) => r.data));
  }

  // ── Enquiry ──────────────────────────────────────────────────────
  createEnquiry(payload: CreateEnquiryRequest): Observable<EnquiryResponse> {
    return this.http
      .post<ApiResponse<EnquiryResponse>>(`${this.base}/enquiry`, payload)
      .pipe(map((r) => r.data));
  }

  // ── WhatsApp Tracking ────────────────────────────────────────────
  trackWhatsApp(payload: TrackWhatsAppRequest): Observable<void> {
    return this.http.post<void>(`${this.base}/whatsapptracking/track`, payload);
  }
}
