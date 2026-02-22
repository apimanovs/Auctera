# Load and resilience smoke checks

## Prerequisites
- k6 installed locally.
- Running API (`backend/host/Auctera.Host`).

## Smoke read load (50-100 RPS)
```bash
k6 run backend/tests/load/smoke-read.js -e BASE_URL=http://localhost:5047
```

## Bid spike / anti-abuse check
```bash
k6 run backend/tests/load/bid-spike.js \
  -e BASE_URL=http://localhost:5047 \
  -e AUCTION_ID=<auction-guid> \
  -e BIDDER_ID=<user-guid> \
  -e TOKEN=<jwt>
```
Expected behavior: mix of `200` and `429` responses under peak, without `5xx`.

## Race condition near auction end
1. Start auction with end time in ~30-60 seconds.
2. Run `bid-spike.js` against that auction for 1 minute.
3. Validate:
   - no accepted bids after auction ended;
   - highest bid remains consistent in auction details;
   - no `5xx` in logs.

## Auto-stop background job under load
During read + bid load, monitor host logs and ensure background `AuctionAutoStopBackgroundService` keeps stopping expired auctions.
