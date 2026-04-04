export interface ServiceSummary {
  id: number;
  slug: string;
  title: string;
  tagline: string;
  shortDescription: string;
  iconEmoji: string;
}

export interface ServiceDetail extends ServiceSummary {
  longDescription: string;
  heroImageUrl?: string;
  keyFeatures: string[];
  techStack: string[];
}
