import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
  scenarios: {
    smoke_read: {
      executor: 'constant-arrival-rate',
      rate: 75,
      timeUnit: '1s',
      duration: '2m',
      preAllocatedVUs: 20,
      maxVUs: 100,
    },
  },
  thresholds: {
    http_req_failed: ['rate<0.01'],
    http_req_duration: ['p(95)<500'],
  },
};

const BASE_URL = __ENV.BASE_URL || 'http://localhost:5047';

export default function () {
  const res = http.get(`${BASE_URL}/api/auctions`);
  check(res, {
    'status is 200': (r) => r.status === 200,
  });
  sleep(0.1);
}
