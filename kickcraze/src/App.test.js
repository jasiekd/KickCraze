import React from 'react';
import { render, screen} from '@testing-library/react';
import '@testing-library/jest-dom/extend-expect';
import MatchSchedule from './components/MatchSchedule';

jest.mock('./controllers/MatchController', () => ({
  GetMatches: jest.fn(() => Promise.resolve([])),
}));

describe('MatchSchedule Component', () => {
  it('displays loading spinner when loading', async () => {
    const setIsLoadingMock = jest.fn();
    render(<MatchSchedule isLoading={true} matchesData={[]} setIsLoading={setIsLoadingMock}/>);
    expect(screen.getByTestId('loader')).toBeInTheDocument();
    expect(screen.getByText('Trwa ładowanie meczy')).toBeInTheDocument();
  });

  it('displays no matches text when no matches are available', async () => {
    const setIsLoadingMock = jest.fn();
    render(<MatchSchedule isLoading={false} matchesData={[]} setIsLoading={setIsLoadingMock}/>);
    expect(screen.getByText('Brak meczy przy aktualnych filtrach')).toBeInTheDocument();
  });
});