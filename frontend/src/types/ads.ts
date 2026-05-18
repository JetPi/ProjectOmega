export interface CampaignSummary {
    id: string;
    name: string;
    platform: 'Google' | 'Microsoft';
    spend: number;
    impressions: number;
}