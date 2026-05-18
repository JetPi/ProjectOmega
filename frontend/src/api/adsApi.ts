import axios from 'axios';
import type { CampaignSummary } from '../types/ads';

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'https://localhost:5215/api';

export const fetchAllCampaigns = async (): Promise<CampaignSummary[]> => {
    const { data } = await axios.get(`${API_BASE_URL}/campaigns/unified`);
    return data;
};