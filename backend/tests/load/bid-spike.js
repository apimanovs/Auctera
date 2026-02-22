import http from 'k6/http';
import { check } from 'k6';

export const options = {
  scenarios: {
    bids_spike: {
      executor: 'ramping-arrival-rate',
      startRate: 5,
      timeUnit: '1s',
      preAllocatedVUs: 20,
      maxVUs: 100,
      stages: [
        { target: 10, duration: '30s' },
        { target: 40, duration: '1m' },
        { target: 5, duration: '30s' },
      ],
    },
  },
  thresholds: {
    http_req_failed: ['rate<0.02'],
  },
};

const BASE_URL = __ENV.BASE_URL || 'http://localhost:5047';
const AUCTION_ID = __ENV.AUCTION_ID;
const TOKEN = __ENV.TOKEN;
const BIDDER_ID = __ENV.BIDDER_ID;

export default function () {
  if (!AUCTION_ID || !TOKEN || !BIDDER_ID) {
    throw new Error('AUCTION_ID, TOKEN, BIDDER_ID env vars are required');
  }

  const payload = JSON.stringify({
    bidderId: BIDDER_ID,
    amount: 1100,
    currency: 'USD',
  });

  const res = http.post(`${BASE_URL}/api/bids/place/${AUCTION_ID}`, payload, {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${TOKEN}`,
    },
  });

  check(res, {
    'bid accepted or throttled': (r) => r.status === 200 || r.status === 429,
  });
}
