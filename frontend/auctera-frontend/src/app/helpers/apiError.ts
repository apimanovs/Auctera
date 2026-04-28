type ApiErrorPayload = {
  message?: string
  detail?: string
  title?: string
  errors?: Record<string, string[]>
}

export function getApiErrorMessage(error: unknown, fallback: string): string {
  const responseData = (error as { response?: { data?: ApiErrorPayload | string } })?.response?.data

  if (typeof responseData === 'string') {
    return responseData.trim() || fallback
  }

  if (!responseData) {
    return fallback
  }

  if (responseData.message) {
    return responseData.message
  }

  if (responseData.detail) {
    return responseData.detail
  }

  const firstValidationError = responseData.errors
    ? Object.values(responseData.errors).flat().find(Boolean)
    : undefined

  return firstValidationError ?? responseData.title ?? fallback
}
