import type {
  ProfileSettingsDto,
  UpdateProfileSettingsPayload,
  UserProfileDto,
} from '@/types/userProfile'
import api from '@/api'

export function ProfileService() {
  async function getUserProfile(username: string): Promise<UserProfileDto> 
  {
    const response = await api.get(`/api/profile/${username}`)
    return response.data
  }

  return {
    getUserProfile,
  }
}

export const profileService = {
  async getUserProfile(username: string): Promise<UserProfileDto> {
    const response = await api.get<UserProfileDto>(`/api/profile/${username}`)
    return response.data
  },

  async getSettings(): Promise<ProfileSettingsDto> {
    const response = await api.get<ProfileSettingsDto>('/api/profile/me/settings')
    return response.data
  },

  async updateSettings(payload: UpdateProfileSettingsPayload): Promise<ProfileSettingsDto> {
    const response = await api.put<ProfileSettingsDto>('/api/profile/me/settings', payload)
    return response.data
  },
}
